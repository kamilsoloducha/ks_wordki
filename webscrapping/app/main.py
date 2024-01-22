from fastapi import FastAPI
from app.cambridge import cambridge
from app.diki import diki

app = FastAPI()

@app.get("/cambridge/{searching_term}")
def cambridge_seach(searching_term: str):
    return cambridge(searching_term)


@app.get("/diki/{searching_term}")
def diki_seach(searching_term: str):
    return diki(searching_term)