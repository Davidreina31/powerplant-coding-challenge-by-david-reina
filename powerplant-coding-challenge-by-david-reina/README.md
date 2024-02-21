# powerplant-coding-challenge


## Welcome !

This project is a solution to the Powerplant Coding Challenge. It exposes a REST API endpoint to calculate the power production plan based on the provided payload.

## How to Run

### 1. HTTP

To run the API in HTTP, follow these steps:

```bash
dotnet run --urls=http://localhost:8888

The API will be accessible at http://localhost:8888.

### 2. HTTPS

To run the API in HTTPS, use the following command:

dotnet run --urls=https://localhost:8888

The API will be accessible at https://localhost:8888.

### 3. Docker
To run the API using Docker, build the Docker image and run a container with the following commands:

docker build -t powerplant-api .
docker run -p 8888:8888 -t powerplant-api

The API will be accessible at http://localhost:8888.


### API Endpoint

Once the API is running, you can access the production plan calculation endpoint:
POST http://localhost:8888/productionplan

Example Payload:
{
  "load": 910,
  "fuels": {
    "gas(euro/MWh)": 13.4,
    "kerosine(euro/MWh)": 50.8,
    "co2(euro/ton)": 20,
    "wind(%)": 60
  },
  "powerplants": [
    {
      "name": "gasfiredbig1",
      "type": "gasfired",
      "efficiency": 0.53,
      "pmin": 100,
      "pmax": 460
    },
    // ... (other powerplants)
  ]
}

Ensure your payload adheres to the specified structure.

