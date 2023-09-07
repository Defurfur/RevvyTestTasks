namespace RevvyTasks.Models;

public struct DirectedGraphTable
{
    public DirectedGraphTable(List<(int clerk, int cert)> dependencies)
    {
        var graphValues = dependencies
            .SelectMany(x => new List<int>(4) { x.cert, x.clerk })
            .Distinct()
            .Order()
            .ToList();


        _map = new();

        for (var i = 0; i < graphValues.Count; i++)
        {
            _map.Add(graphValues[i], i);
        }

        GraphSideSize = graphValues.Count;

        _graph = CreateGraphTable(dependencies, GraphSideSize);
    }

    private Dictionary<int, int> _map;
    private int[,] _graph;
    public int GraphSideSize { get; private set; }

    public List<int> CreateChain()
    {
        List<int> chain = new(GraphSideSize + 1);

        while(GraphSideSize != 0)
        {

            var elements = FindIndependentElements();

            chain.AddRange(elements);

            DeleteIndependentElements(elements);
        }

        return chain;
    }

    private IEnumerable<int> FindIndependentElements()
    {
        List<int> elements = new(GraphSideSize);

        for (var row = 0; row < GraphSideSize; row++)
        {
            for (var cell = 0; cell < GraphSideSize; cell++)
            {
                if (_graph[row, cell] == 1)
                {
                    goto Outer;
                }

            }

            if (_map.ContainsValue(row))
            {
                elements.Add(_map.First(x => x.Value == row).Key);
            }


        Outer: continue;

        }

        return elements;
    }

    private void DeleteIndependentElements(IEnumerable<int> elements)
    {
        foreach (var key in elements)
        {
            DeleteIndependentElement(key);
        }
    }
    private void DeleteIndependentElement(int key)
    {
        var value = _map.GetValueOrDefault(key);

        for (var row = 0; row < GraphSideSize; row++)
        {
            _graph[row, value] = 0;
        }

        _map.Remove(key, out _);

        if (!_map.Any())
        {
            GraphSideSize = 0;
            return;
        }

        GraphSideSize = _map.LastOrDefault().Value + 1;
    }

    private int[,] CreateGraphTable(List<(int clerk, int cert)> dependencies, int graphSize)
    {

        int[,] graphTable = new int[graphSize, graphSize];

        foreach (var (clerk, cert) in dependencies)
        {
            graphTable[_map.GetValueOrDefault(clerk), _map.GetValueOrDefault(cert)] = 1;
        }

        return graphTable;
    }

}