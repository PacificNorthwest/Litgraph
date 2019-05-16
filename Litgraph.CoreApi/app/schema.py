import graphene
from app.data import dal
from graphene_abstracted import AbstractedMutation

class CharacterScheme(graphene.ObjectType):
    id = graphene.String()
    name = graphene.String()

class LocationScheme(graphene.ObjectType):
    id = graphene.String()
    title = graphene.String()

class MaterialSchema(graphene.ObjectType):
    id = graphene.String()
    title = graphene.String()

    characters = graphene.List(CharacterScheme)
    locations = graphene.List(LocationScheme)

class UserSchema(graphene.ObjectType):
    id = graphene.String()
    username = graphene.String()
    email = graphene.String()
    materials = graphene.List(MaterialSchema)

class UserMutation(AbstractedMutation):
    class Arguments:
        email = graphene.String()

    successful = graphene.Boolean()
    errors = graphene.List(graphene.String)
class Query(graphene.ObjectType):
    user = graphene.Field(UserSchema, email=graphene.String())

    def resolve_user(self, info, email):
        return dal.resolve_user(info.context, email)

schema = graphene.Schema(query=Query)