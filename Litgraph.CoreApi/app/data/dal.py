
from py2neo.database import Graph
from py2neo.ogm import GraphObject, Property, RelatedTo

def get_graph_context():
    return Graph(auth=('user', 'pass'))

class Character(GraphObject):
    __primarykey__ = 'name'

    name = Property()
    brief = Property()
    
class Location(GraphObject):
    __primarykey__ = 'title'

    title = Property()
    brief = Property()

class Material(GraphObject):
    __primarykey__ = 'title'

    title = Property()
    brief = Property()
    
    locations = RelatedTo(Location, 'LOCATES')
    characters = RelatedTo(Character, 'INCLUDES')

class User(GraphObject):
    __primarykey__ = 'email'

    username = Property()
    email = Property()

    materials = RelatedTo(Material, 'OWNS')

def resolve_user(context, email):
    return User.match(context['Graph'], email).first()

