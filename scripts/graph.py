class Graph(dict):
    def __init__(self, vertices):
        self.vertices = vertices

    def add_edge(self, v1, v2, w):
        if v1 in self.vertices and v2 in self.vertices:
            self[(lambda v1, v2: (v1, v2) if v1 < v2 else (v2, v1))(v1, v2)] = w

    def remove_edge(self, v1, v2):
        del self[(lambda v1, v2: (v1, v2) if v1 < v2 else (v2, v1))(v1, v2)]
