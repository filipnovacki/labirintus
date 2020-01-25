from math import sqrt
def input_data(vertices_names, heuristic):
    edges = []
    vertices = []
    start = ''
    end = ''

    with open("data/bridovi.txt") as f:
        edges = [x[:-1].split(',') for x in f.readlines()[1:]]

    with open("data/vrhovi.txt") as f:
        vertices = [x[:-1].split(',') for x in f.readlines()[1:]]
        start = min(vertices, key = lambda x: int(x[2]))[0]
        end_list = max(vertices, key = lambda x: int(x[2]))
        end = end_list[0]
        if heuristic == 'sqrt':
            vertices = [x + [sqrt((int(x[2]) - int(end_list[2]))**2 +\
                (int(x[1]) - int(end_list[1]))**2)] for x in vertices]
        elif heuristic == 'pow':
            vertices = [x + [(int(x[2]) - int(end_list[2]))**2 + (int(x[1]) -\
                int(end_list[1]))**2] for x in vertices]

    if vertices_names:
        with open("data/surnames.csv") as s:
            names = [x[:-1].split(',')[0].capitalize() for x in s.readlines()[1:]]
        dictionary = dict(zip([x[0] for x in vertices], names))

        for x in vertices:
            x[0] = dictionary[x[0]]
        for x in edges:
            x[0] = dictionary[x[0]]
            x[1] = dictionary[x[1]]

        start = dictionary[start]
        end = dictionary[end]

    edges = [(a[0], a[1], {'weight': a[2]}) for a in edges]

    return (vertices, edges, start, end)

def populate_graph(vertices_names=False, heuristic = 'sqrt'):
    data = input_data(vertices_names, heuristic)
    import networkx as nx
    G = nx.Graph()
    G.add_edges_from(data[1])
    for n in data[0]:
        G.add_node(n[0], coords = (n[1], n[2]), h=n[3])
    return G, data[2], data[3]

