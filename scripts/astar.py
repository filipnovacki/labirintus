def a_min_dist(G, dist, spath):
    min = float('inf')
    min_index = None

    for node in G.nodes:
        if dist[node][0] + dist[node][1] < min and\
                spath[node] is False:
            min = dist[node][0] + dist[node][1]
            min_index = node
    return min_index

def astar(G, src, end):
    distances = {x:[float('inf'), G.nodes[x]['h']] for x in G.nodes}
    print(distances)
    distances[src][0] = 0
    spath = {}.fromkeys(list(G.nodes), False)

    for _ in G.nodes:
        u = a_min_dist(G, distances, spath)
        if distances[u][0] >= distances[end][0]:
            return distances
        spath[u] = True
        print(u, spath)
        for v in G.nodes:
            if G.has_edge(u, v) and spath[v] is False and\
                    distances[v][0] > distances[u][0] + G.edges[u,v]['weight']:
                distances[v][0] = distances[u][0]+G.edges[u,v]['weight']

    return distances






