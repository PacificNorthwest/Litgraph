import graphene
from flask import Flask
from flask_graphql import GraphQLView
from app.mutations import Mutations
from app.queries import Query
from app.data.dal import get_graph_context

schema = graphene.Schema(query=Query, mutation=Mutations)

def create_app():
    app = Flask(__name__)
    app.add_url_rule(
        '/graphql',
        view_func=GraphQLView.as_view(
            'graphql',
            schema = schema,
            graphiql = True,
            get_context = lambda: { 'Graph': get_graph_context() }
        )
    )
    return app
