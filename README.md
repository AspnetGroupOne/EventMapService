# EventMapService

# Postman:

## Authentication:

All requests to this API require an API-Key to be passed in the header under "X-API-KEY". 

Invalid requests will be met with:

```json
{
    "success": false,
    "error": "Invalid api-key or api-key is missing."
}
```

## POST and PUT: 

Is made by sending a request with no id at the end. 

www.EXAMPLEURL.net/api/Maps/

The api needs data that looks like this:

```json
{
    "eventId": "56f58514-7581-4b18-97f5-b6eb5ba7b9c9",
    "imageUrl": "https://cdn.example.com/maps/event-123.png",
    "nodes": [
        {
            "nodeType": "start",
            "gridId": 1
        },
        {
            "nodeType": "checkpoint",
            "gridId": 5
        },
        {
            "nodeType": "finish",
            "gridId": 10
        }
    ]
}
```

## GET:

Is made by sending a request with an id at the end. 

www.EXAMPLEURL.net/api/Maps/56f58514-7581-4b18-97f5-b6eb5ba7b9c9

And you will recieve data in json format that looks like this on success:

```json
{
    "content": {
        "eventId": "56f58514-7581-4b18-97f5-b6eb5ba7b9c9",
        "imageUrl": "https://cdn.example.com/maps/event-123.png",
        "nodes": [
            {
                "nodeType": "start",
                "gridId": 1
            },
            {
                "nodeType": "checkpoint",
                "gridId": 5
            },
            {
                "nodeType": "finish",
                "gridId": 10
            }
        ]
    },
    "success": true,
    "statusCode": 200,
    "message": null
}
```

## DELETE

Is made by sending a request with an id at the end. 

www.EXAMPLEURL.net/api/Maps/56f58514-7581-4b18-97f5-b6eb5ba7b9c5

Upon success you will recieve:

```json
{
    "success": true,
    "statusCode": 200,
    "message": null
}
```

# Sequence diagram plantuml

<img src="https://github.com/user-attachments/assets/ee0fd56a-978e-4088-b3f7-4a727a4bc291" width="400">

# Usage in the frontend:

Coming soon..

### Created By:

https://github.com/SimonR-prog
