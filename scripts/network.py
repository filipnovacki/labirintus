import matplotlib.pyplot as plt
import networkx as nx

def networker(graph):
    G = nx.Graph()

    G.add_nodes_from(graph.vertices)

    for v1, v2 in graph.keys():
        G.add_edge(v1, v2)

    return G

def plot_graph(graph):
    nx.draw(networker(graph))
    plt.show()

