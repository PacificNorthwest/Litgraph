from py2neo.ogm import GraphObject, Property, RelatedTo

class Character(GraphObject):
    __primarykey__ = 'id'

    id = Property()
    name = Property()
    brief = Property()
    
class Location(GraphObject):
    __primarykey__ = 'id'

    id = Property()
    title = Property()
    brief = Property()

class Material(GraphObject):
    __primarykey__ = 'id'

    id = Property()
    title = Property()
    brief = Property()
    
    locations = RelatedTo(Location, 'LOCATES')
    characters = RelatedTo(Character, 'INCLUDES')

class User(GraphObject):
    __primarykey__ = 'email'

    username = Property()
    email = Property()

    materials = RelatedTo(Material, 'OWNS')
