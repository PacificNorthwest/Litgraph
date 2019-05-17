class NodeNotFoundError(Exception):
    def __init__(self):
        super().__init__("Requested entity not found")

class UserNotFoundError(NodeNotFoundError):
    def __init__(self):
        super().__init__("Requested user not found")