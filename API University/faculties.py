from typing import Dict,List,Optional
import random
from pydantic import BaseModel
from fastapi import APIRouter
from db import DATABASE
from directions import Direction
from staff_file import Staff

class FacultyAPI:
    facultyRouter = APIRouter()

    @staticmethod
    def give_exception(
         university_id: int,
         faculty_id: Optional[int] = None
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
        else:
            return 'exist something error'
    @staticmethod
    @facultyRouter.post('/create',summary='Создание факультета')
    def create_faculty(
        university_id: int,
        faculty_name: str,
        directions: List[str] | None = None, 
    ):
        faculty = Faculty.create(faculty_name,directions)
        try:
            DATABASE[university_id].faculties[faculty.id] = faculty
            return faculty.read()
        except:
            return FacultyAPI.give_exception(university_id)
    @staticmethod
    @facultyRouter.post('/generate',summary='Генерация факультетов')
    def generate_faculty(
        university_id: int,
        faculties_count: int = 5,
        directions_count: int = 5,
        groups_count: int = 2,
        students_count: int = 15,
        subjects_count: int = 5
    ):
        faculties = Faculty.generate(
            faculties_count,directions_count,groups_count,students_count,subjects_count
        )
        try:
            university= DATABASE[university_id]
            university.faculties.update(faculties)
            university.add_people_for_clubs(list(faculties.keys()))
        except:
            return FacultyAPI.give_exception(university_id)
        info = FacultyDB()
        for i in faculties:
            info.faculties.append(faculties[i])
        return info.dict()
    @staticmethod
    @facultyRouter.get('/{faculty_id}', summary='Чтение факультета')
    def read_faculty_info(
         university_id: int,
         faculty_id: int
    ):
        try:
            faculty = DATABASE[university_id].faculties[faculty_id]
            return faculty.read()
        except:
            return FacultyAPI.give_exception(university_id, faculty_id)
    @staticmethod
    @facultyRouter.delete('/{faculty_id}', summary='Удаление факультета')
    def delete_faculty(
         university_id: int,
         faculty_id: int,
    ):
        try:
            DATABASE[university_id].faculties.pop(faculty_id)
            return {'responce_code': 200}
        except:
            return FacultyAPI.give_exception(university_id, faculty_id)
          
class Faculty(BaseModel):
    id: int
    faculty_name: str
    directions: Dict[int,Direction] = {}
    staff: Dict[int,Staff] = {}
    __count__ = 0
    __LIST__ = []

    @staticmethod
    def create(
        faculty_name: str,
        directions: List[str] | None = None, 
    ):
        id = Faculty.__count__
        new_faculty = Faculty(id=id, faculty_name=faculty_name)

        if directions:
            new_faculty.directions = {}
            for i in range(len(directions)):
                direction = Direction.create(directionName=directions[i])
                new_faculty.directions[direction.id] = direction               

        Faculty.__count__ += 1
        return new_faculty
        
    def read(self): return self.dict()
        
    @staticmethod
    def generate(
        faculties_count: int = 5,
        directions_count: int = 5,
        groups_count: int = 2,
        students_count: int = 15,
        subjects_count: int = 5,
    ) -> dict:
        
        def read_file():
            with open('app\\FileDataBases\\facultyDB.txt', encoding='utf-8') as f:
                for line in f:
                    Faculty.__LIST__.append(line.strip())

        if not Faculty.__LIST__: 
            read_file()
        temporary_list_of_faculties = Faculty.__LIST__.copy()

        faculty_dict = {}
        for i in range(faculties_count):
            id = Faculty.__count__
            faculty_name = random.choice(temporary_list_of_faculties)
            faculty = Faculty(id=id,faculty_name=faculty_name)
            staff_count = directions_count * groups_count * subjects_count // 5 + 1
            faculty.staff = Staff.generate(staff_count)
            faculty.directions = Direction.generate(
                faculty.staff,directions_count,groups_count,students_count,subjects_count
            )
            faculty_dict[faculty.id] = faculty
            temporary_list_of_faculties.remove(faculty_name)
            Faculty.__count__ += 1
        return faculty_dict

class FacultyDB(BaseModel):
    faculties: List[Faculty] = []
    
