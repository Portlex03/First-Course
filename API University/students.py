from typing import Dict,List,Optional
import random
from pydantic import BaseModel
from fastapi import APIRouter
from db import DATABASE

class StudentAPI:
    studentRouter = APIRouter()

    @staticmethod
    def give_exception(
         university_id: int,
         faculty_id: int,
         direction_id: int,
         group_id: int,
         student_id: Optional[int] = None
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
        elif group_id not in DATABASE[university_id].faculties[faculty_id].directions[direction_id].groups:
            return {
                'responce_code': 404, 
                'group_id': 'not found'
            }
        elif student_id and student_id not in DATABASE[university_id].faculties[faculty_id].directions[direction_id].groups[group_id].students:
            return {
                'responce_code': 404, 
                'student_id': 'not found'
            }
        else:
            return 'exist something error'
    
    @staticmethod
    @studentRouter.post('/create',summary='Создание студента')
    def create_student(
        university_id: int,
        faculty_id: int,
        direction_id: int,
        group_id: int,
        studentName: str
    ):
        try:
            university = DATABASE[university_id]
            faculty = university.faculties[faculty_id]
            direction = faculty.directions[direction_id]
            group = direction.groups[group_id]
            student = Student.create(studentName)
            group.students[student.id] = student
            group.give_marks(student.id)
        except:
            return StudentAPI.give_exception(university_id,faculty_id,direction_id,group_id)

    @staticmethod
    @studentRouter.post('/generate',summary='Генерация студентов')
    def generate_students(
        university_id: int,
        faculty_id: int,
        direction_id: int,
        group_id: int,
    ):
        try:
            university = DATABASE[university_id]
            faculty = university.faculties[faculty_id]
            direction = faculty.directions[direction_id]
            group = direction.groups[group_id]
            students = Student.generate()
            group.students.update(students)
            group.give_marks(list(students.keys()))
        except:
            return StudentAPI.give_exception(university_id,faculty_id,direction_id,group_id)
        info = StudentDB()
        for i in students:
            info.students.append(students[i])
        return info.dict()
    @staticmethod
    @studentRouter.get("/{student_id}", summary='Чтение студента')
    def read_student_info(
        university_id: int,
        faculty_id: int,
        direction_id: int,
        group_id: int,
        student_id: int
    ):
        try:
            university = DATABASE[university_id]
            faculty = university.faculties[faculty_id]
            direction = faculty.directions[direction_id]
            group = direction.groups[group_id]
            student = group.students[student_id]
            return student.read()
        except:
            return StudentAPI.give_exception(university_id,faculty_id,direction_id,group_id,student_id)
    
class Student(BaseModel):
    id: int
    studentName: str
    diary: Dict[str,int] = {}
    club: str = None
    __count__ = 0
    __LIST__ = []

    @staticmethod
    def create(
        studentName: str
    ):
        id = Student.__count__
        new_student = Student(id=id,studentName=studentName)
        Student.__count__ += 1
        return new_student

    def read(self): return self.dict()

    @staticmethod
    def generate(
        studentCount: int = 15
    ):
        def read_file():
            with open('app\\FileDataBases\\peopleDB.txt', encoding='utf-16') as f:
                for line in f:
                    Student.__LIST__.append(line.strip())

        # чтение файла
        if not Student.__LIST__: read_file()
        # создание временного списка
        temporary_list_of_students = Student.__LIST__.copy()
        
        stud_dict = {}
        for i in range(studentCount):
            id = Student.__count__
            studentName = random.choice(temporary_list_of_students)
            student = Student(id=id,studentName=studentName)
            temporary_list_of_students.remove(studentName)
            stud_dict[student.id] = student
            Student.__count__ += 1
        return stud_dict
    
class StudentShortInfo(BaseModel):
    id: int
    studentName: str
    mark: int = -1

class StudentDB(BaseModel):
    students: List[Student]
