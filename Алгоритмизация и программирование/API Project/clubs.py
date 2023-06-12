from typing import List,Dict,Optional
import random
from pydantic import BaseModel
from fastapi import APIRouter
from db import DATABASE

class ClubAPI:
    clubRouter = APIRouter()

    @staticmethod
    def give_exception(
        university_id: int,
        club_id: Optional[int] = None
    ):
        if university_id not in DATABASE:
            return {'responce_code': 404, 'university_id': 'not found'}
        elif club_id and club_id not in DATABASE[university_id].clubs:
            return {'responce_code': 404, 'club_id': 'not found'}
        else:
            return 'exists somethig error'
    @staticmethod
    @clubRouter.post("/create",summary='Создание клуба')
    def create_club(
        university_id: int, 
        club_name: str, 
        days_of_event: List[str] | None = None
    ):
        club = Club.create(club_name, days_of_event)
        try:
            DATABASE[university_id].clubs[club.id] = club
            return club.read()
        except:
            return ClubAPI.give_exception(university_id)
    @staticmethod
    @clubRouter.post('/generate',summary='Генерация клуба')
    def generate_club(
        university_id: int,
        count: int = 10
    ):
        clubs = Club.generate(count)
        try:
            DATABASE[university_id].clubs = clubs
        except:
            return ClubAPI.give_exception(university_id)
        info = ClubDB()
        for i in clubs.keys():
            info.clubs.append(clubs[i])
        return info.dict()
    @staticmethod
    @clubRouter.get('/{club_id}',summary='Чтение информации о клубе')
    def read_club_info(
        university_id: int,
        club_id: int 
    ):
        try:
            club =  DATABASE[university_id].clubs[club_id]
            return club.read()
        except:
            return ClubAPI.give_exception(university_id, club_id)
    @staticmethod
    @clubRouter.delete('/{club_id}',summary='Удаление информации о клубе')
    def delete_club(
        university_id: int,
        club_id: int
    ):
        try:
            DATABASE[university_id].clubs.pop(club_id)
            return {'responce_code': 200}
        except:
            return ClubAPI.give_exception(university_id, club_id)
        

class Club(BaseModel):
    id: int
    club_name: str
    days_of_event: List[str] = []
    students: Dict[int,str] = {}
    __count__ = 0
    __LIST__ = []

    @staticmethod
    def create(
        club_name: str,
        days_of_event: List[str] | None = None,
    ):
        id = Club.__count__
        new_club = Club(id=id,club_name=club_name)
        if days_of_event: 
            new_club.days_of_event=days_of_event
        Club.__count__ += 1
        return new_club
    def read(self): 
        return self.dict()
    @staticmethod
    def generate(count: int = 10) -> dict:

        def generate_days() -> list:
            DAYS = [ 'Понедельник', 'Вторник', 'Среда', 'Четверг', 'Пятница', 'Суббота']
            chousen_days = random.sample(DAYS,3)
            return chousen_days
        
        def read_file():
            with open('app/FileDataBases/clubDB.txt', encoding='utf-8') as f:
                for line in f:
                    Club.__LIST__.append(line.strip())

        if not Club.__LIST__: 
            read_file()
        temporary_list_of_clubs = Club.__LIST__.copy()

        clubs_dict = {}
        for i in range(count):
            id = Club.__count__
            club_name = random.choice(temporary_list_of_clubs)
            days_of_event = generate_days()
            club = Club(
                id=id,club_name=club_name,days_of_event=days_of_event
            )
            clubs_dict[club.id] = club
            Club.__count__ += 1
        return clubs_dict
class ClubDB(BaseModel):
    clubs: List[Club] = []
    
