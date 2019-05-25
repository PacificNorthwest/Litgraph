import uuid
from py2neo.database import Graph
from app.data import nodes
from app.exceptions import NodeNotFoundError, UserNotFoundError

def get_graph_context(config):
    if config is None:
        return Graph(auth=('neo4j', 'pass'), host='localhost')

    login = config.get('NEO4J_LOGIN', 'neo4j')
    password = config.get('NEO4J_PASS', 'pass')
    host = config.get('NEO4J_HOST', 'localhost')

    return Graph(auth=(login, password), host=host)


def resolve_user(context, email):
    user = nodes.User.match(context['Graph'], email).first()
    if user is None:
        raise UserNotFoundError()
    
    return user

def resolve_materials(context, user_email):
    user = resolve_user(context, user_email)
    return user.materials

def create_material(context, email, title, brief):
    try:
        user = resolve_user(context, email)
    except UserNotFoundError:
        user = nodes.User()
        user.email = email

    material = nodes.Material()
    material.id = str(uuid.uuid4())
    material.title = title
    material.brief = brief
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
    


