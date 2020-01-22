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
    from anytree import Node
    distances = {x:[float('inf'), G.nodes[x]['h']] for x in G.nodes}
    distances[src][0] = 0
    spath = {}.fromkeys(list(G.nodes), False)
    prev = {x:Node(x) for x in G.nodes}
    prev[src] = Node(src)
    for _ in G.nodes:
        u = a_min_dist(G, distances, spath)
        if distances[u][0] >= distances[end][0] or \
                u is end:
            return distances, prev
        spath[u] = True
        for v in G.nodes:
            if G.has_edge(u, v) and spath[v] is False and\
                    distances[v][0] > distances[u][0] +\
                    int(G.edges[u,v]['weight']):
                distances[v][0] = distances[u][0]+int(G.edges[u,v]['weight'])
                prev[v].parent = prev[u]

    return distances, prev
