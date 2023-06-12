from typing import Dict, List, Optional
import random
from pydantic import BaseModel
from fastapi import APIRouter
from students import StudentShortInfo
from db import DATABASE

class SubjectAPI:
    subjectRouter = APIRouter()

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
    @subjectRouter.post('/subjects/generate', summary='генерация предметов для направления')
    def generate_subjects(
        university_id: int,
        faculty_id: int,
        direction_id: int,
        subjects_count: int = 10
    ):
        subjects = Subject.generate(subjects_count)
        try:
            direction = DATABASE[university_id].faculties[faculty_id].directions[direction_id]
            direction.subjects = subjects
        except:
            return SubjectAPI.give_exception(university_id, faculty_id, direction_id)
        info = SubjectsDB()
        for i in direction.subjects:
            info.subjects.append(direction.subjects[i])
        return info.dict()

    @staticmethod
    @subjectRouter.get('/{group_id}/subjects/{subjects_id}', summary='Чтение предмета группы')
    def read_subject_info(
        university_id: int,
        faculty_id: int,
        direction_id: int,
        group_id: int,
        subject_id: int
    ):
        try:
            university = DATABASE[university_id]
            faculty = university.faculties[faculty_id]
            direction = faculty.directions[direction_id]
            group = direction.groups[group_id]
            subject = group.subjects[subject_id]
            return subject.read()
        except:
            return SubjectAPI.give_exception(university_id, faculty_id, direction_id, group_id)


class Subject(BaseModel):
    id: int
    subject_name: str
    staff: str = None
    group: str = None
    student_marks: Dict[int, StudentShortInfo] = {}
    __count__ = 0
    __LIST__ = []

    @staticmethod
    def create(subject_name: str):
        id = Subject.__count__
        new_subject = Subject(id=id, subject_name=subject_name)

        Subject.__count__ += 1
        return new_subject

    def read(self):
        return self.dict()

    @staticmethod
    def generate(subjects_count: int = 5) -> list:
        def read_file():
            with open('app/FileDataBases/subjectsDB.txt', encoding='utf-8') as f:
                for line in f:
                    Subject.__LIST__.append(line.strip())
        if not Subject.__LIST__:
            read_file()
        temporary_list_of_subjects = Subject.__LIST__.copy()

        subject_list = []
        for i in range(subjects_count):
            subject_name = random.choice(temporary_list_of_subjects)
            subject_list.append(subject_name)
            temporary_list_of_subjects.remove(subject_name)
        return subject_list


class SubjectsDB(BaseModel):
    subjects: List[str] = []
