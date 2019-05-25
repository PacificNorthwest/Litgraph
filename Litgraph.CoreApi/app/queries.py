import graphene
from app.schema import UserSchema, MaterialSchema
from app.exceptions import UserNotFoundError
from app.data import dal

class Query(graphene.ObjectType):
    user = graphene.Field(UserSchema, email=graphene.String(required=True))
    materials = graphene.List(MaterialSchema, user_email=graphene.String(required=True))

    def resolve_user(self, info, email):
        return dal.resolve_user(info.context, email)

    def resolve_materials(self, info, user_email):
        try:
            return dal.resolve_materials(info.context, user_email)
        except UserNotFoundError:
            return []
