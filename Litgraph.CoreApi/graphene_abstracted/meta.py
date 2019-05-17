from graphene.utils.subclass_with_meta import SubclassWithMeta_Meta
from graphene.types.base import BaseType
from graphene.types.structures import Structure 
from .abstracted_base import AbstractedGrapheneBaseType

class AbstractedMutationMeta(SubclassWithMeta_Meta):
    def __new__(meta, name, bases, class_dict):
        if name.startswith('None'):
            return None

        for base in bases:
            if issubclass(base, AbstractedGrapheneBaseType):
                if 'Arguments' in base.__dict__ and 'Arguments' in class_dict:
                    for key, value in base.Arguments.__dict__.items():
                        if key not in class_dict['Arguments'].__dict__:
                            setattr(class_dict['Arguments'], key, value)
                
                for key, value in base.__dict__.items():
                    if issubclass(type(value), (BaseType, Structure)):
                        class_dict[key] = value
                
        cls = type.__new__(meta, name, bases, class_dict)
        
        return cls