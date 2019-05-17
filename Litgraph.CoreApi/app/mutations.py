import graphene
from app import schema
from app.data import dal
from graphene_abstracted import AbstractedMutation

class BaseMutation(AbstractedMutation):
    successful = graphene.Boolean()
    errors = graphene.List(graphene.String)

class UserMutation(BaseMutation):
    class Arguments:
        email = graphene.String()

class CreateMaterial(graphene.Mutation, UserMutation):
    class Arguments:
        title = graphene.String(required=True)
    
    material = graphene.Field(lambda: schema.MaterialSchema)

    def mutate(self, info, email, title):
        try:
            material = dal.create_material(info.context, email, title)
            return CreateMaterial(successful=True, material=material)
        except Exception as e:
            return CreateMaterial(successful=False, errors=[str(e)])

class CreateCharacter(graphene.Mutation, BaseMutation):
    class Arguments:
        material_id = graphene.String()
        name = graphene.String(required=True)
        brief = graphene.String()

    character = graphene.Field(lambda: schema.CharacterSchema)

    def mutate(self, info, material_id, name, brief):
        try:
            character = dal.create_character(info.context, material_id, name, brief)
            return CreateCharacter(successful=True, character=character)
        except Exception as e:
            return CreateCharacter(successful=False, errors=[str(e)])

class CreateLocation(graphene.Mutation, BaseMutation):
    class Arguments:
        material_id = graphene.String()
        title = graphene.String(required=True)
        brief = graphene.String()

    location = graphene.Field(lambda: schema.LocationSchema)

    def mutate(self, info, material_id, title, brief):
        try:
            location = dal.create_location(info.context, material_id, title, brief)
            return CreateLocation(successful=True, location=location)
        except Exception as e:
            return CreateLocation(successful=False, errors=[str(e)])
    

class Mutations(graphene.ObjectType):
    create_material = CreateMaterial.Field()
    create_character = CreateCharacter.Field()
    create_location = CreateLocation.Field()