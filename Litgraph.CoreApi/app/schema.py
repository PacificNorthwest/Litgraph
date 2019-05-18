import graphene

class CharacterSchema(graphene.ObjectType):
    id = graphene.ID(required=True)
    name = graphene.String()
    brief = graphene.String()

class LocationSchema(graphene.ObjectType):
    id = graphene.ID(required=True)
    title = graphene.String()
    brief = graphene.String()

class MaterialSchema(graphene.ObjectType):
    id = graphene.ID(required=True)
    title = graphene.String()
    brief = graphene.String()

    characters = graphene.List(CharacterSchema)
    locations = graphene.List(LocationSchema)

class UserSchema(graphene.ObjectType):
    username = graphene.String()
    email = graphene.String()
    materials = graphene.List(MaterialSchema)