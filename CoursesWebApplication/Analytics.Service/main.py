from fastapi import FastAPI
from analytics import (
    get_daily_stats,
    get_total_income_per_course,
    get_purchases_by_day,
    get_top_5_courses,
    export_all_events_csv,
    export_purchase_csv
)
import kafka_consumer


app = FastAPI()

@app.get("/")
def root():
    return {"message": "Kafka consumer running..."}

@app.on_event("startup")
async def startup_event():
    kafka_consumer.start_kafka_consumer()

@app.get("/analytics/daily")
def daily_stats():
    return get_daily_stats()

@app.get("/analytics/income")
def income_per_course():
    return get_total_income_per_course()

@app.get("/analytics/purchases")
def purchases_by_day():
    return get_purchases_by_day()

@app.get("/analytics/top")
def top_5_courses():
    return get_top_5_courses()

@app.get("/analytics/export-all")
def export_all():
    return export_all_events_csv()

@app.get("/analytics/export-purchases")
def export_purchases():
    return export_purchase_csv()
