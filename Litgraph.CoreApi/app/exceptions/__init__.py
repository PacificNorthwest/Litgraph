class NodeNotFoundError(Exception):
    def __init__(self, message=None):
        super(NodeNotFoundError, self).__init__(message or "Requested entity not found")

class UserNotFoundError(NodeNotFoundError):
    def __init__(self):
        super(UserNotFoundError, self).__init__("Requested user not found")