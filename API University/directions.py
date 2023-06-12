from typing import Dict,List,Optional
import random
from pydantic import BaseModel
from fastapi import APIRouter
from db import DATABASE
from groups import Group
from subjects import Subject
from staff_file import Staff

class DirectionAPI:
    dirRouter = APIRouter()

    @staticmethod
    def give_exception(
        university_id: int,
        faculty_id: int,
        direction_id: Optional[int] = None
    ):
        if university_id not in DATABASE:
            return {'responce_code': 404, 'university_id': 'not found'}
        elif faculty_id not in DATABASE[university_id].faculties:
            return {'responce_code': 404, 'faculty_id': 'not found'}
        elif direction_id and direction_id not in DATABASE[university_id].faculties[faculty_id]:
            return {'responce_code': 404, 'direction_id': 'not found'}
        else:
            return 'exist something error'
    @staticmethod
    @dirRouter.post('/create',summary='Создание направления')
    def create_direction(
        university_id: int,
        faculty_id: int,
        direction_code: str,
        direction_name: str,
        groups: List[str] | None = None,
        subjects: List[str] | None = None
    ):
        direction = Direction.create(
            direction_name,direction_code,
            groups,subjects
        )
        try:
            DATABASE[university_id].faculties[faculty_id].directions[direction.id] = direction
            return direction.read()
        except:
            return DirectionAPI.give_exception(university_id,faculty_id)
    @staticmethod
    @dirRouter.post('/generate',summary='Генерация направления')
    def generate_direction(
        university_id: int,
        faculty_id: int,
        directions_count: int = 5,
        groups_count: int = 3,
        students_count: int = 25,
        subjects_count: int = 10,
    ):
        try:
            staff = DATABASE[university_id].faculties[faculty_id].staff
            directions = Direction.generate(
                staff,directions_count,groups_count,students_count,subjects_count
            )
            DATABASE[university_id].faculties[faculty_id].directions = directions
        except:
            return DirectionAPI.give_exception(university_id,faculty_id)
        info = DirectionDB()
        for i in directions.keys():
            info.directions.append(directions[i])
        return info.dict()
    @staticmethod
    @dirRouter.get('/{direction_id}',summary='Чтение направления')
    def read_direction(
        university_id: int,
        faculty_id: int,
        direction_id: int
    ):
        try:
            direction = DATABASE[university_id].faculties[faculty_id].directions[direction_id]
            return direction.read()
        except:
            return DirectionAPI.give_exception(university_id,faculty_id,direction_id)
        
class Direction(BaseModel):
    id: int
    direction_name: str
    subjects: List[str] = []
    groups: Dict[int,Group] = {}
    __count__ = 0
    __LIST__ = []

    @staticmethod
    def create(
        direction_name: str,
        direction_code: str = None,
        groups: List[str] | None = None,
        subjects: List[str] | None = None
    ):
        id = Direction.__count__
        if not direction_code: 
            direction_code = str(Direction.__count__ * 10) + '.' + '00 '
        direction_name = direction_code + ' ' + direction_name
        new_direction = Direction(id=id,direction_name=direction_name)

        if groups:
            new_direction.groups = {}
            for i in range(len(groups)):
                group = Group.create(group_name=groups[i])
                new_direction.groups[group.id] = group

        if subjects:
            new_direction.subjects = subjects

        Direction.__count__ += 1
        return new_direction

    def read(self): return self.dict()

    @staticmethod
    def generate(
        staff: Dict,
        direction_count: int = 5,
        groups_count: int = 3,
        students_count: int = 25,
        subjects_count: int = 10,
    ):
        def read_file():
            with open('app/FileDataBases/dir_namesDB.txt', encoding='utf-16') as f:
                for line in f:
                    Direction.__LIST__.append(line.strip())

        if not Direction.__LIST__: read_file()
        temporary_list_of_dir = Direction.__LIST__.copy()

        direction_dict = {}
        for i in range(direction_count):
            id = Direction.__count__
            direction_name = random.choice(temporary_list_of_dir)

            direction = Direction(id=id,direction_name=direction_name)
            direction.subjects = Subject.generate(subjects_count)
            direction.groups = Group.generate(direction_name,groups_count,students_count)
            direction.append_subjects()

            Staff.find_work(
                groups=direction.groups,
                staff=staff,
                subjects_count=len(direction.subjects)
            )

            for group in direction.groups.values():
                group.give_marks()
            direction_dict[direction.id] = direction
            temporary_list_of_dir.remove(direction.direction_name)
            Direction.__count__ += 1
        return direction_dict

    def append_subjects(self, groups_id: Optional[int] | list = None):
        if not self.subjects:
            self.subjects = Subject.generate() 
        if self.groups and not groups_id:
            for group in self.groups.values():
                group.subjects = {}
                for i in range(len(self.subjects)):
                    subject = Subject.create(subject_name=self.subjects[i])
                    subject.group = group.group_name
                    group.subjects[subject.id] = subject

        elif type(groups_id) == int:
            group = self.groups[groups_id]
            group.subjects = {}
            for i in range(len(self.subjects)):
                subject = Subject.create(subject_name=self.subjects[i])
                subject.group = group.group_name
                group.subjects[subject.id] = subject

        elif type(groups_id) == list:
            for groups_id in groups_id:
                group = self.groups[groups_id]
                group.subjects = {}
                for i in range(len(self.subjects)):
                    subject = Subject.create(subject_name=self.subjects[i])
                    subject.group = group.group_name
                    group.subjects[subject.id] = subject
    
class DirectionDB(BaseModel):
    directions: List[Direction] = []
    
