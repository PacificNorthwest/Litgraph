import graphene
from app.schema import UserSchema
from app.data import dal

class Query(graphene.ObjectType):
    user = graphene.Field(UserSchema, email=graphene.String())

    def resolve_user(self, info, email):
        return dal.resolve_user(info.context, email)