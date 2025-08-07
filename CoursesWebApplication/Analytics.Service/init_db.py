from database import Base, engine
from models import CourseEvent

Base.metadata.create_all(bind=engine)
