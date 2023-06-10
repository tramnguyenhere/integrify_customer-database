namespace DatabaseManagement;

class FileHelper
{
    private string _path { get; set; }
    private FileInfo _fileInfo;

    public FileHelper(string path)
    {
        _path = path;
        _fileInfo = new FileInfo(path);
    }
    public void CreateCustomerFile()
    {
        if (!File.Exists(_path))
        {
            File.Create(_path).Close();
        }
    }
    public string[]? GetAll()
    {
        try
        {
            var data = File.ReadAllLines(_path);
            return data;
        }
        catch (Exception e)
        {
            throw ExceptionHandler.FetchDataException(e.Message);
        }
    }

    public void AddNew(string content)
    {
        File.AppendAllText(_path, content);
    }
    public async Task AddNewAsync(string content)
    {
        using (var sw = _fileInfo.AppendText())
        {
            await sw.WriteLineAsync(content);
        }
    }
}