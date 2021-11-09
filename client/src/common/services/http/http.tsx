import axios from "axios";

export const API_PATH = "http://localhost:5000";

const instance = axios.create({
  baseURL: API_PATH,
  headers: {
    "Content-type": "application/json",
  },
});

export default instance;
