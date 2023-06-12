from typing import List,Dict,Optional
import random
from pydantic import BaseModel
from fastapi import APIRouter
from students import Student
from subjects import Subject
from db import DATABASE

class GroupAPI:
    groupRouter = APIRouter(prefix="/group", tags=['Group'])

    @staticmethod
    def give_exception(
         university_id: int,
         faculty_id: int,
         direction_id: int,
         group_id: Optional[int] = None
    ):
        if university_id not in DATABASE:
            return {
                'responce_code': 404, 
                'university_id': 'not found'
            }
        elif faculty_id not in DATABASE[university_id].faculties:
            return {
                'responce_code': 404, 
                'faculty_id': 'not found'
            }
        elif direction_id not in DATABASE[university_id].faculties[faculty_id].directions:
            return {
                'responce_code': 404, 
                'direction_id': 'not found'
            }
        elif group_id and group_id not in \
        DATABASE[university_id].faculties[faculty_id].directions[direction_id].groups:
            return {
                'responce_code': 404, 
                'group_id': 'not found'
            }
        else:
            return 'exists something error'
    @staticmethod
    @groupRouter.post("/create",summary='Создание группы')
    def create_group(
        university_id: int,
        faculty_id: int,
        direction_id: int,
        group_name: str,
        group_students: List[str] | None = None
    ):
        try:
            university = DATABASE[university_id]
            faculty = university.faculties[faculty_id]
            direction = faculty.directions[direction_id]
            group = Group.create(group_name,group_students)
            direction.groups[group.id] = group
            direction.append_subjects(group.id)
            return group.read()
        except:
            return GroupAPI.give_exception(university_id,faculty_id,direction_id)
    @staticmethod  
    @groupRouter.post('/generate',summary='Генерация группы')
    def generate_group(
        university_id: int,
        faculty_id: int,
        direction_id: int,
        group_count: int = 2, 
        students_count: int = 15,
    ):
        try:
            university = DATABASE[university_id]
            faculty = university.faculties[faculty_id]
            direction = faculty.directions[direction_id]
            groups = Group.generate(direction.directionName,group_count,students_count)
            direction.groups.update(groups)
            direction.append_subjects(list(groups.keys()))
        except:
            return GroupAPI.give_exception(university_id,faculty_id,direction_id)
        info = GroupDB()
        for i in groups:
            info.groups.append(groups[i])
        return info.dict()
    @staticmethod
    @groupRouter.get("/{group_id}",summary='Чтение информации группы')
    def read_group_info(
        university_id: int,
        faculty_id: int,
        direction_id: int,
        group_id: int
    ):
        try:
            university = DATABASE[university_id]
            faculty = university.faculties[faculty_id]
            direction = faculty.directions[direction_id]
            group = direction.groups[group_id]
            return group.read()
        except:
            return GroupAPI.give_exception(university_id,faculty_id,direction_id,group_id)

class Group(BaseModel):
    id: int
    group_name: str
    students: Dict[int,Student] = {}
    subjects: Dict[int,Subject] = {}
    __count__ = 0

    @staticmethod
    def create(
        group_name: str,
        students_list: List[str] | None = None
    ):
        id = Group.__count__
        new_group = Group(id=id,group_name=group_name)
        
        if students_list:
            for i in range(len(students_list)):
                student = Student.create(studentName=students_list[i])
                new_group.students[student.id] = student
        Group.__count__ += 1
        return new_group
    def read(self): 
        return self.dict()
    @staticmethod
    def generate(
      dir_name: str, 
      group_count: int = 2, 
      students_count: int = 15
    ):
        group_dict = {}
        for i in range(group_count):
            id = Group.__count__
            # Имя группы - заглавные первые буквы имени направления + уникальный номер
            group_name = ''.join([word[0] for word in dir_name.split()[1:]]).upper() + '-' + str(Group.__count__)
            group = Group(id=id,group_name=group_name)
            group.students = Student.generate(students_count)
            group_dict[group.id] = group
            Group.__count__ += 1
        return group_dict
    
    def give_marks(self, students: List[int] | Optional[int] = None):
        if self.students and not students:
            for student in self.students.values():
                student.diary = {}
                for subject in self.subjects.values():
                    mark = random.randint(2,5)
                    student.diary[subject.subjectName] = mark
                    subject.student_marks[student.id].mark = mark
        elif type(students) == int:
            student = self.students[students]
            student.diary = {}
            for subject in self.subjects.values():
                mark = random.randint(2,5)
                student.diary[subject.subjectName] = mark
                subject.student_marks[student.id].mark = mark
        elif type(students) == List[int]:
            for student_id in students:
                student = self.students[student_id]
                student.diary = {}
                for subject in self.subjects.values():
                    mark = random.randint(2,5)
                    student.diary[subject.subjectName] = mark
                    subject.student_marks[student.id].mark = mark

class GroupDB(BaseModel):
    groups: List[Group] = []
    
