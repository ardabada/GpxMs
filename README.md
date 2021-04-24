# GpxMs
Microservices architecture for GPX file creation. Practice project for USM.

![Preview](https://raw.githubusercontent.com/ardabada/GpxMs/master/scheme.png)


| Service | HTTPS | HTTP |
| ----------- | ----------- | ----------- |
| API Gate | https://localhost:5010/ | http://localhost:5000/
| Geo Service | https://localhost:5011/ | http://localhost:5001/
| Visualization Service | https://localhost:5012/ | http://localhost:5002/
| GPX Registry Service | https://localhost:5013/ | http://localhost:5003/

Launch:
`start launch.bat`

Sample request:
```
POST http://localhost:5000/build HTTP/1.1
Content-Type: application/json
Host: localhost:5000
Content-Length: 1347

{
   "tracks": [
      {
         "coords": [
            { "lat": 47.0177310, "long": 28.8240051 },
            { "lat": 47.0170288, "long": 28.8246274 },
            { "lat": 47.0186234, "long": 28.8266873 },
            { "lat": 47.0206860, "long": 28.8296056 },
            { "lat": 47.0235970, "long": 28.8338113 },
            { "lat": 47.0239042, "long": 28.8339615 },
            { "lat": 47.0252791, "long": 28.8340473 },
            { "lat": 47.0256009, "long": 28.8341117 },
            { "lat": 47.0260544, "long": 28.8348413 },
            { "lat": 47.0263615, "long": 28.8349700 },
            { "lat": 47.0268588, "long": 28.8355923 },
            { "lat": 47.0276341, "long": 28.8367939 },
            { "lat": 47.0283946, "long": 28.8379097 },
            { "lat": 47.0286433, "long": 28.8386178 },
            { "lat": 47.0295647, "long": 28.8399696 },
            { "lat": 47.0296525, "long": 28.8401198 },
            { "lat": 47.0288773, "long": 28.8413858 },
            { "lat": 47.0291844, "long": 28.8420510 },
            { "lat": 47.0293746, "long": 28.8434029 },
            { "lat": 47.0296269, "long": 28.8447011 },
            { "lat": 47.0295867, "long": 28.8450927 }
         ]
      }
   ],
   "start": "2021-04-26T10:00:00Z",
   "splits": [
      "2021-04-26T10:30:00Z"
   ]
}


```
