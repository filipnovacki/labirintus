import networkx as nx
import scripts.dijkstra

def start():
    G = nx.Graph()
    edges = [
            ('a', 'b', 3),
            ('b', 'd', 5),
            ('a', 'c', 4),
            ('c', 'd', 2),
            ('c', 'e', 1),
            ('e', 'f', 1),
            ('c', 'b', 4),
            ('d', 'f', 8)]
    G.add_weighted_edges_from(edges)
    return G

G = start()

scripts.dijkstra.dijkstra(G, 0)
