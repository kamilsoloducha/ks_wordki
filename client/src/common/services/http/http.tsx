import axios from "axios";

// export const API_PATH = "http://localhost:5000";
export const API_PATH = "https://wordki-server.herokuapp.com";

const instance = axios.create({
  baseURL: API_PATH,
  headers: {
    "Content-type": "application/json",
  },
});

export default instance;
