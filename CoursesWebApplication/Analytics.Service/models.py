from sqlalchemy import Column, String, DateTime, Integer, Float
from sqlalchemy.dialects.mssql import UNIQUEIDENTIFIER
from datetime import datetime
from database import Base

class CourseEvent(Base):
    __tablename__ = "course_events"

    id = Column(Integer, primary_key=True)
    event_type = Column(String) 
    course_id = Column(UNIQUEIDENTIFIER)
    course_name = Column(String)
    user_id = Column(UNIQUEIDENTIFIER, nullable=True)  
    price = Column(Float, nullable=True)
    timestamp = Column(DateTime, default=datetime.utcnow)
