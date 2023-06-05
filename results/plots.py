import os
import json
import matplotlib.pyplot as plt

# Read data from file:
results_dir_path = r'C:\Users\mivva\Desktop\Projekty\c#\DataStructures\results'
json_path = os.path.join(results_dir_path,'performanceData.json')

with open(json_path, 'r') as f:
    data = json.load(f)

# Functions to get data for specific operation
def get_data(operation):
    return [{"DataType": d["DataType"], "DataSize": d["DataSize"], "Time": d[operation]} for d in data]

# Generate plots for each operation with different scales
def plot_data(operation, scale_type="linear"):
    op_data = get_data(operation)
    plt.figure(figsize=(10,6))
    for datatype in set(d['DataType'] for d in op_data):
        x = [d['DataSize'] for d in op_data if d['DataType'] == datatype]
        y = [d['Time'] for d in op_data if d['DataType'] == datatype]
        plt.plot(x, y, label=datatype[:-2])

    plt.xlabel('Data Size')
    plt.ylabel('Time [ms]')
    plt.title(f'{operation[:-4]} method vs Data Size')
    plt.grid(True)
    plt.legend()

    if scale_type == 'log':
        plt.yscale('log')  # Set y-axis scale to logarithmic
        plt.savefig(os.path.join(results_dir_path, f'{operation[:-4]}_plot_log.jpg'))
    else:
        plt.savefig(os.path.join(results_dir_path, f'{operation[:-4]}_plot.jpg'))


if __name__ == '__main__':
    plot_data('PutTime')
    plot_data('GetTime')
    plot_data('ContainsKeyTime')
    plot_data('RemoveTime')

    plot_data('PutTime', scale_type='log')
    plot_data('GetTime', scale_type='log')
    plot_data('ContainsKeyTime', scale_type='log')
    plot_data('RemoveTime', scale_type='log')