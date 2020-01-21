def clean_graph(G, src, end):
    delete_one = []
    for node in G:
        if len(G[node]) is 1 and\
                node != src and\
                node != end:
            delete_one.append(node)
    G.remove_nodes_from(delete_one)

    delete_two = []
    for node in G:
        if len(G[node]) is 2:
            weight_sum = sum([int(G[node][a]['weight']) for a in G[node]])
            G.add_edge(*G[node], weight = weight_sum)
            delete_two.append(node)
    G.remove_nodes_from(delete_two)

    return G

