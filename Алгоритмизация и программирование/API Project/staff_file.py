from pydantic import BaseModel
from fastapi import APIRouter
from typing import Dict,List,Optional
import random

from db import DATABASE
from subjects import Subject
from students import StudentShortInfo

class StaffAPI:
    staffRouter = APIRouter()

    @staticmethod
    def give_exception(
         university_id: int,
         faculty_id: Optional[int] = None,
         direction_id: Optional[int] = None
    ):
        if university_id not in DATABASE:
            return {
                'responce_code': 404, 
                'university_id': 'not found'
            }
        elif faculty_id and faculty_id not in DATABASE[university_id].faculties:
            return {
                'responce_code': 404, 
                'faculty_id': 'not found'
            }
        elif direction_id and direction_id not in DATABASE[university_id].faculties[faculty_id].directions:
            return {
                'responce_code': 404, 
                'direction_id': 'not found'
            } 
        else:
            return 'exists something error'
    @staticmethod
    @staffRouter.post('/create',summary='Создание преподавателя')
    def create_staff(
        university_id: int,
        faculty_id: int,
        staff_name: str
    ):
        try:
            faculty = DATABASE[university_id].faculties[faculty_id]
            staff = Staff.create(staff_name)
            faculty.staff[staff.id] = staff
            return staff.dict()
        except:
            return StaffAPI.give_exception(university_id,faculty_id)
    @staticmethod
    @staffRouter.post('/generate',summary='генерация преподавателя')
    def generate_staff(
        university_id: int,
        faculty_id: int,
        staffCount: int
    ):
        try:
            faculty = DATABASE[university_id].faculties[faculty_id]
            staff = Staff.generate(staffCount)
            faculty.staff.update(staff)
        except:
            return StaffAPI.give_exception(university_id,faculty_id)
        info = StaffDB()
        for i in faculty.staff:
            info.staff.append(faculty.staff[i])
        return info.dict()
    @staticmethod
    @staffRouter.get('/{staff_id}',summary='Чтение преподавателя')
    def read_staff(
        university_id: int,
        faculty_id: int,
        staff_id: int
    ):
        try:
            university = DATABASE[university_id]
            faculty = university.faculties[faculty_id]
            staff = faculty.staff[staff_id]
            return staff.read()
        except:
            return StaffAPI.give_exception(university_id,faculty_id,staff_id)
    @staticmethod
    @staffRouter.post('/find_work',summary='Соединение преподавателей с группой')
    def find_work_to_staff(
        university_id: int,
        faculty_id: int,
        direction_id: int
    ):
        try:
            university = DATABASE[university_id]
            faculty = university.faculties[faculty_id]
            direction = faculty.directions[direction_id]
            Staff.find_work(
                groups=direction.groups,
                staff=faculty.staff,
                subjects_count=len(direction.subjects)
            )
        except:
            return StaffAPI.give_exception(university_id,faculty_id,direction_id)

class Staff(BaseModel):
    id: int
    staff_name: str
    subjects: List[Subject] = []
    __count__ = 0
    __LIST__ = []

    @staticmethod
    def create(staff_name: str):
        id = Staff.__count__
        new_staff = Staff(id=id,staff_name=staff_name)
        Staff.__count__ += 1
        return new_staff
    def read(self): 
        return self.dict()
    @staticmethod
    def generate(count: int):

        def read_file():
            with open('app/FileDataBases/peopleDB.txt',encoding='utf-16') as f:
                for line in f:
                    Staff.__LIST__.append(line.strip())  
        if not Staff.__LIST__: 
            read_file()
        temporary_list_of_staff = Staff.__LIST__.copy()

        staff_dict = {}
        for i in range(count):
            id = Staff.__count__
            staff_name = random.choice(temporary_list_of_staff)
            staff = Staff(id=id,staff_name=staff_name)

            temporary_list_of_staff.remove(staff_name)
            staff_dict[staff.id] = staff
            Staff.__count__ += 1
        return staff_dict
    @staticmethod
    def find_work(
        groups: Dict,
        staff: Dict,
        subjects_count: int
    ):
        # преподаватели меньше чем с 5ю предметами
        free_stuff = [person for person in staff.values() if len(person.subjects) < 5]
        # сумма предметов, которые могут взять преподаватели
        staff_can_take_subjects = sum([5 - len(person.subjects) for person in free_stuff])
        # количество предметов, у которых должен быть преподаватель
        count_subjects = len(groups) * subjects_count
        # если преподавателей меньше, чем надо, то добавляем
        if not staff or staff_can_take_subjects < count_subjects:
            count_new_staff = (count_subjects - staff_can_take_subjects) // 5 + 1
            staff.update(Staff.generate(count_new_staff))

        # лист преподавателей, у которых менее 5 предметов
        staff_id_list = [person for person in staff if len(staff[person].subjects) < 5]
        for group in groups.values():
            if group.subjects:
                for subject in group.subjects.values():
                    if not subject.staff:
                        # выбор случайного преподавателя
                        index = random.randint(0,len(staff_id_list) - 1)
                        staff_id = staff_id_list[index]
                        # соединение предмета с преподавателем
                        subject.staff = staff[staff_id].staff_name
                        # соединение предмета с группой
                        if group.students:
                            for student in group.students.values():
                                new_student = StudentShortInfo(
                                    id=student.id,
                                    studentName=student.studentName,
                                )
                                subject.student_marks[new_student.id] = new_student
                            # соединение преподавателя с предметом группы
                            staff[staff_id].subjects.append(subject)

class StaffDB(BaseModel):
    staff: List[Staff] = []
