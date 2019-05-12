import graphene
from app.data import dal

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

class Query(graphene.ObjectType):
    user = graphene.Field(UserSchema, email=graphene.String())

    def resolve_user(self, info, email):
        return dal.resolve_user(info.context, email)

schema = graphene.Schema(query=Query)