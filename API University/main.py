from fastapi import FastAPI
import uvicorn

from universities import UniversityAPI
from clubs import ClubAPI
from faculties import FacultyAPI
from staff_file import StaffAPI
from directions import DirectionAPI
from groups import GroupAPI
from subjects import SubjectAPI
from students import StudentAPI

app = FastAPI()
app.include_router(
    router=UniversityAPI.uniRouter,
    prefix='/university',
    tags=['University']
)
app.include_router(
    router=ClubAPI.clubRouter,
    prefix="/university/{university_id}/clubs",
    tags=['Clubs']
)
app.include_router(
    router=FacultyAPI.facultyRouter, 
    prefix="/university/{university_id}/faculties",
    tags=['Faculty']
)
app.include_router(
    router=StaffAPI.staffRouter,
    prefix='/university/{university_id}/faculties/{faculty_id}/staff',
    tags=['Staff']
)
app.include_router(
    router=DirectionAPI.dirRouter,
    prefix="/university/{university_id}/faculties/{faculty_id}/directions",
    tags=['Direction']
)
app.include_router(
    router=GroupAPI.groupRouter,
    prefix="/university/{university_id}/faculties/{faculty_id}/directions/{direction_id}/groups",
    tags=['Group']
)
app.include_router(
    router=SubjectAPI.subjectRouter,
    prefix="/university/{university_id}/faculties/{faculty_id}/directions/{direction_id}",
    tags=['Subject']
)
app.include_router(
    router=StudentAPI.studentRouter,
    prefix="/university/{university_id}/faculties/{faculty_id}/directions/{direction_id}/groups/{group_id}/students",
    tags=['Student']
)

if __name__ == '__main__':
    uvicorn.run(app)