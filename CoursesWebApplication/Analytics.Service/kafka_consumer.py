from kafka import KafkaConsumer
import json
import threading
import uuid
from models import CourseEvent
from database import SessionLocal
from datetime import datetime

def process_message(message):
    session = SessionLocal()
    data = json.loads(message.value.decode("utf-8"))

    try:
        event = CourseEvent(
            event_type=data.get("EventType"),
            course_id=uuid.UUID(data.get("CourseId")),
            course_name=data.get("CourseName"),
            user_id=uuid.UUID(data["UserId"]) if data.get("UserId") else None,
            price=data.get("Price"),
            timestamp=datetime.fromisoformat(data.get("Timestamp").replace("Z", "+00:00"))
        )
        session.add(event)
        session.commit()
        print(f"Evento {data.get('EventType')} guardado.")
    except Exception as e:
        session.rollback()
        print("Error al guardar evento:", e)
    finally:
        session.close()

def start_kafka_consumer():
    def run():
        consumer = KafkaConsumer(
            "course_events",
            bootstrap_servers="localhost:9092",
            group_id="analytics-group",
            auto_offset_reset="earliest"
        )
        print("Escuchando eventos en Kafka...")
        for message in consumer:
            print("Mensaje procesado:", message.value.decode("utf-8"))
            process_message(message)

    thread = threading.Thread(target=run)
    thread.daemon = True
    thread.start()
