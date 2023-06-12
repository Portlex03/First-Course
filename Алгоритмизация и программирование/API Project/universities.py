from pydantic import BaseModel
from fastapi import APIRouter
from typing import Dict,List
import random
from db import DATABASE
from faculties import Faculty
from clubs import Club

class UniversityAPI:
    uniRouter = APIRouter()

    @staticmethod
    @uniRouter.post('/create',summary='Создание университета')
    def create_university(
        uni_name: str, 
        faculties: List[str] | None = None, 
        clubs: List[str] | None = None
    ):
        university = University.create(uni_name,faculties,clubs)
        DATABASE[university.id] = university
        return university.read()
    
    @staticmethod
    @uniRouter.post('/generate',summary='Генерация университета',description='Работает дольше, чем панировалось')
    def generate_university(
        faculties_сount: int = 5,
        clubs_сount: int = 10,
        directions_сount: int = 5,
        groups_count: int = 2,
        students_count: int = 15,
        subjects_count: int = 5
    ):
        university = University.generate(
            faculties_сount,clubs_сount,directions_сount,groups_count,students_count,subjects_count
        )
        DATABASE[university.id] = university
        return university.read()
    
    @staticmethod
    @uniRouter.get('/{university_id}',summary='Чтение университета')
    def read_university_info(university_id: int):
        try:
            university = DATABASE[university_id]
            return university.read()
        except:
            return {'responce_code': 404, 'university_id': 'not found'}
    
    @staticmethod
    @uniRouter.delete("/{university_id}",summary='Удаление университета')
    def delete_university(university_id: int):
        try:
            DATABASE.pop(university_id)
            return {'responce_code': 200}
        except:
            return {'responce_code': 404, 'university_id': 'not found'}

class University(BaseModel):
    id: int
    uni_name: str
    faculties: Dict[int,Faculty] = {}
    clubs: Dict[int,Club] = {}
    __count__ = 0
    __LIST__ = []

    @staticmethod
    def create(
        uni_name: str,
        faculties: List[str] | None = None,
        clubs: List[str] | None = None
    ):
        id = University.__count__
        new_university = University(id=id,uni_name=uni_name)

        if faculties:
            new_university.faculties = {}
            while faculties:
                faculty = Faculty.create(facultyName=faculties[0])
                new_university.faculties[faculty.id] = faculty
                faculties.pop(0)
        if clubs:
            new_university.clubs = {}
            while clubs:
                club = Club.create(clubName=clubs[0])
                new_university.clubs[club.id] = club
                clubs.pop(0)

        University.__count__ += 1
        return new_university
    
    def read(self): return self.dict()
    
    @staticmethod
    def generate(
        faculties_сount: int = 5,
        clubs_сount: int = 10,
        directions_сount: int = 5,
        groups_count: int = 2,
        students_count: int = 15,
        subjects_count: int = 5
    ):
        def read_file():
            with open('app\\FileDataBases\\uni_namesDB.txt', encoding='utf-16') as f:
                for line in f:
                    University.__LIST__.append(line.strip())
        if not University.__LIST__: 
            read_file()
        id = University.__count__
        uni_name = random.choice(University.__LIST__)
        new_university = University(id=id,uni_name=uni_name)
        new_university.clubs = Club.generate(clubs_сount)
        new_university.faculties = Faculty.generate(
            faculties_сount,directions_сount,groups_count,students_count,subjects_count,
        )
        new_university.add_people_for_clubs()
        University.__count__ += 1
        return new_university
    def add_people_for_clubs(self, faculties: List[int] = None):
        if self.clubs:
            if not faculties:
                for faculty in self.faculties.values():
                    for direction in faculty.directions.values():
                        for group in direction.groups.values():
                            for student in group.students.values():
                                will_in_club = bool(random.randint(0,1))
                                if will_in_club:
                                    club_id = random.choice(list(self.clubs.keys()))
                                    student.club = self.clubs[club_id].clubName
                                    self.clubs[club_id].students[student.id] = student.studentName
            else:
                for faculty_id in faculties:
                    faculty = self.faculties[faculty_id]
                    for direction in faculty.directions.values():
                        for group in direction.groups.values():
                            for student in group.students.values():
                                will_in_club = bool(random.randint(0,1))
                                if will_in_club:
                                    club = random.choice(self.clubs)
                                    student.club = club.clubName
                                    club.students[student.id] = student.studentName

class DataBase(BaseModel):
    data: List[University] = []
