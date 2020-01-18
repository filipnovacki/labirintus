def min_dist(G, dist, spath):
    min = float('inf')
    min_index = None
    for node in G.nodes:
        if dist[node] < min and spath[node] is False:
            min = dist[node]
            min_index = node
    return min_index

def dijkstra(G, src):
    distances = {}.fromkeys(list(G.nodes), float('inf'))
    distances[src] = 0
    spath = {}.fromkeys(list(G.nodes), False)
    for _ in G.nodes:
        u = min_dist(G, distances, spath)
        spath[u] = True
        for v in G.nodes:
            if G.has_edge(u, v) and spath[v] is False and\
                    distances[v] > distances[u] + G.edges[u, v]['weight']:
                distances[v] = distances[u] + G.edges[u, v]['weight']
    return distances
