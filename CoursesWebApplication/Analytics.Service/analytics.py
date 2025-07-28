from models import CourseEvent
from database import SessionLocal
from collections import defaultdict, Counter
from datetime import datetime
import csv

def get_daily_stats():
    session = SessionLocal()
    events = session.query(CourseEvent).all()
    stats = defaultdict(int)

    for event in events:
        key = f"{event.course_name} - {event.timestamp.date()}"
        stats[key] += 1

    session.close()
    return dict(stats)

def get_total_income_per_course():
    session = SessionLocal()
    events = session.query(CourseEvent).filter(CourseEvent.event_type == "PURCHASED").all()
    income = defaultdict(float)

    for event in events:
        income[event.course_name] += event.price or 0

    session.close()
    return dict(income)

def get_purchases_by_day():
    session = SessionLocal()
    events = session.query(CourseEvent).filter(CourseEvent.event_type == "PURCHASED").all()
    purchases = defaultdict(int)

    for event in events:
        date = event.timestamp.date()
        purchases[str(date)] += 1

    session.close()
    return dict(purchases)

def get_top_5_courses():
    session = SessionLocal()
    events = session.query(CourseEvent).filter(CourseEvent.event_type == "PURCHASED").all()
    counter = Counter(event.course_name for event in events)
    top_courses = counter.most_common(5)

    session.close()
    return [{"course_name": name, "purchases": count} for name, count in top_courses]

def export_all_events_csv():
    session = SessionLocal()
    events = session.query(CourseEvent).all()

    with open("all_events_export.csv", "w", newline="") as f:
        writer = csv.writer(f)
        writer.writerow(["Event Type", "Course Name", "User ID", "Price", "Timestamp"])
        for e in events:
            writer.writerow([
                e.event_type,
                e.course_name,
                getattr(e, "user_id", ""),
                e.price or 0,
                e.timestamp
            ])

    session.close()
    return {"message": "CSV con todos los eventos exportado correctamente"}

def export_purchase_csv():
    session = SessionLocal()
    events = session.query(CourseEvent).filter(CourseEvent.event_type == "PURCHASED").all()

    with open("purchases_export.csv", "w", newline="") as f:
        writer = csv.writer(f)
        writer.writerow(["Course Name", "User ID", "Price", "Timestamp"])
        for e in events:
            writer.writerow([
                e.course_name,
                getattr(e, "user_id", ""),
                e.price or 0,
                e.timestamp
            ])

    session.close()
    return {"message": "CSV de compras exportado correctamente"}
