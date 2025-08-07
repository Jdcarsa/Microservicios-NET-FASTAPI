from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker, declarative_base

DATABASE_URL = 'mssql+pyodbc://user_test:<wEML_a_7v6Z_MLE>@localhost:1433/AnalyticsDB?driver=ODBC+Driver+17+for+SQL+Server'

engine = create_engine(DATABASE_URL)
SessionLocal = sessionmaker(bind=engine)

Base = declarative_base()