from scripts.graph import DirGraph
from scripts.network import networker, plot_graph, plot_graph_circular,\
plot_graph_kk, plot_graph_planar
import networkx as nx
import matplotlib.pyplot as plt

graph = DirGraph(['a', 'b', 'c', 'd', 'e', 'f'])

graph.add_edge('a', 'b', 5.0)
graph.add_edge('c', 'b')
graph.add_edge('a', 'c')
graph.add_edge('a', 'd')
graph.add_edge('a', 'e')
graph.add_edge('d', 'e')
graph.add_edge('d', 'b')
graph.add_edge('a', 'f')

G = networker(graph)

nx.draw(G, with_labels=True)

plt.show()
