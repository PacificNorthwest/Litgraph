from flask import Flask
from flask_graphql import GraphQLView
from app.schema import schema
from app.data.dal import get_graph_context

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
