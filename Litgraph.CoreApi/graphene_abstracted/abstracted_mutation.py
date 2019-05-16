from .meta import AbstractedMutationMeta
from .abstracted_base import AbstractedGrapheneBaseType
class AbstractedMutation(AbstractedGrapheneBaseType, metaclass=AbstractedMutationMeta):
    class Arguments:
        pass