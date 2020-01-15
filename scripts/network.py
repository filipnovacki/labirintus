import matplotlib.pyplot as plt
import networkx as nx

def networker(graph):
    G = nx.Graph()

    G.add_nodes_from(graph.vertices)

    # {(a, b):3}
    edges = []

    for e in graph:
        edges.append((e[0], e[1], graph[e]))

    G.add_weighted_edges_from(edges)
    return G

def plot_graph(graph):
    nx.draw(networker(graph))
    plt.show()
