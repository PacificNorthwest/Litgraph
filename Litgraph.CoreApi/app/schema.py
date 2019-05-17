import graphene

class CharacterSchema(graphene.ObjectType):
    id = graphene.String()
    name = graphene.String()
    brief = graphene.String()

class LocationSchema(graphene.ObjectType):
    id = graphene.String()
    title = graphene.String()
    brief = graphene.String()

class MaterialSchema(graphene.ObjectType):
    id = graphene.String()
    title = graphene.String()
    brief = graphene.String()

    characters = graphene.List(CharacterSchema)
    locations = graphene.List(LocationSchema)

class UserSchema(graphene.ObjectType):
    id = graphene.String()
    username = graphene.String()
    email = graphene.String()
    materials = graphene.List(MaterialSchema)