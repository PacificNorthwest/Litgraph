import uuid
from py2neo.database import Graph
from app.data import nodes
from app.exceptions import NodeNotFoundError, UserNotFoundError

def get_graph_context():
    return Graph(auth=('neo4j', 'pass'), host='localhost')


def resolve_user(context, email):
    user = nodes.User.match(context['Graph'], email).first()
    if user is None:
        raise UserNotFoundError()
    
    return user

def create_material(context, email, title):
    try:
        user = resolve_user(context, email)
    except UserNotFoundError:
        user = nodes.User()
        user.email = email

    material = nodes.Material()
    material.id = str(uuid.uuid4())
    material.title = title
    user.materials.add(material)

    context['Graph'].push(user)
    return material

def create_character(context, material_id, name, brief):
    material = nodes.Material.match(context['Graph'], material_id).first()
    if material is None:
        raise NodeNotFoundError()

    character = nodes.Character()
    character.id = str(uuid.uuid4())
    character.name = name
    character.brief = brief

    material.characters.add(character)
    context['Graph'].push(material)
    return character

def create_location(context, material_id, title, brief):
    material = nodes.Material.match(context['Graph'], material_id).first()
    if material is None:
        raise NodeNotFoundError()

    location = nodes.Location()
    location.id = str(uuid.uuid4())
    location.title = title
    location.brief = brief

    material.locations.add(location)
    context['Graph'].push(material)
    return location
    


