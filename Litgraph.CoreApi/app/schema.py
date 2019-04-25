import graphene
from app.data import repository

class UserSchema(graphene.ObjectType):
    name = graphene.String()
    id = graphene.String()

class Query(graphene.ObjectType):
    user = graphene.Field(UserSchema, name=graphene.String())

    def resolve_user(self, info, name):
        return repository.User(0, name)

schema = graphene.Schema(query=Query)