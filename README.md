# hockey-game

- [Endpoints](#endpoints)
  - [GET /api/team/{year}](#GET-/api/team/{year})
  - [POST /api/team/{Year}](#POST-/api/team/{year})
  - [PUT /api/player/captain/{ID}](#PUT-/api/player/captain/{ID})


## Endpoints

### GET /api/team/{year}

- Requ�te: Year dans l'URI
- R�ponse: Objet Team (Voir mod�le ci-dessus)
- Status: 200 OK

```
http://localhost:8080/api/team/2020 --header "Content-Type:application/json"

{
    "id": 1,
    "coach": "Dominique Ducharme"
    "year" : 2020
    "players": [
        {
            "number": 99,
            "name": "John",
            "lastname": "Doe",
            "position": "defenseman",
            "isCaptain" : false
        }
        [...]
    ]
}
```

### POST /api/team/{Year}

- Requ�te: Objet Joueur dans le body
- R�ponse: Objet Joueur cr�e
- Status: 201 CREATED

```
http://localhost:8080/api/team/2020 --header "Content-Type:application/json"

{
  "number":99,
  "name":"Antonin",
  "lastname":"Bouscarel",
  "position":"forward",
  "isCaptain" : false
}
```

### PUT /api/player/{ID}/captain/

- Requ�te: ID du joueur dans l'URI
- R�ponse: Objet Player
- Status: 200 OK

```
http://localhost:8080/api/player/9/captain

{
  "number":99,
  "name":"Antonin",
  "lastname":"Bouscarel",
  "position":"forward",
  "isCaptain" : true
}
```