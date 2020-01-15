class Graph(dict):
    def __init__(self, vertices):
        self.vertices = vertices

    def add_vertex(self, vertex):
        (lambda vertex: self.vertices.append(vertex) if type(self.vertices) ==\
            list else self.vertices.add(vertex))(vertex)

    def add_edge(self, v1, v2, w=1):
        if v1 in self.vertices and v2 in self.vertices:
            self[(lambda v1, v2: (v1, v2) if str(v1) < str(v2) else \
                    (v2, v1))(v1, v2)] = int(w)

    def remove_edge(self, v1, v2):
        del self[(lambda v1, v2: (v1, v2) if str(v1) < str(v2) else \
                (v2, v1))(v1, v2)]

    def neighbours(self, v):
        for v1, v2 in list(self.keys()):
            a = (lambda v1, v2, v: v1 if v == v2 else (lambda v1, v2, v: v2\
                if v==v1 else None)(v1, v2, v))(v1, v2, v)
            if a is not None:
                yield a

class DirGraph(Graph):
    def __init__(self, vertices):
        self.vertices = vertices
        # start is on first index of vertices
        self.start = vertices[0]
        # end is on last index of vertices
        self.end = vertices[-1]
