
from py2neo.database import Graph
from .nodes import User, Material

def get_graph_context():
    return Graph(auth=('neo4j', 'pass'), host='localhost')


def resolve_user(context, email):
    return User.match(context['Graph'], email).first()

def create_material(context, email, title):
    user = resolve_user(context, email)
    print(user)
    material = Material()
    material.title = title
    user.materials.add(material)

    context['Graph'].push(user)
    return material

