class Character:
    def __init__(self, id, name):
        self.id = id
        self.name = name

class Location:
    def __init__(self, id, name):
        self.id = id
        self.name = name

class Material:
    def __init__(self, id, title, characters, locations):
        self.id = id
        self.title = title
        self.characters = characters
        self.locations = locations

class User:
    def __init__(self, id, username, email):
        self.id = id
        self.username = username
        self.email = email
        self.materials = [
            Material(0, "Harry Potter", [
                Character(0, "Harry Potter"),
                Character(1, "Hermione Granger")
            ], 
            [
                Location(0, "Hogwarts"),
                Location(1, "London")
            ]),
            Material(1, "Lord of the rings", [
                Character(2, "Frodo Baggins"),
                Character(3, "Sauron")
            ],
            [
                Location(2, "Shire"),
                Location(3, "Minas Tirit")
            ])
        ]

def get_user(email):
    return User(0, 'PacificNorthwest', email)

