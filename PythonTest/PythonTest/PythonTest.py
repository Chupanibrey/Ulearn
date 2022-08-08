import numpy as np
import matplotlib.pyplot as plt

n = 100
b = 35
m = 100
u_0 = 22
u_1 = 35
u_2 = 40
x_1 = 14
x_2 = 21
x_3 = 24
h = b / (n + 1)
c_x_1 = int(x_1 / h)
c_x_2 = int(x_2 / h)
c_x_3 = int(x_3 / h)

cells = np.zeros((m + 1, n + 2))

cells[0] = u_0
cells[0, c_x_1 : c_x_2 + 1] = u_1
cells[0, c_x_2 + 1 : c_x_3 + 1] = u_2
cells[:, 0] = u_0
cells[:, n+1] = u_0

def get_next_cell(cells, step, curr_cell):
	cell = sum(cells[step, curr_cell - 1 : curr_cell + 2])/3
	return cell

def run_cells(cells, n, m):
	for step in range(1,m + 1):
	    for i in range(1,n + 1):
			cells[step, i] = get_next_cell(cells, step - 1, i)
	return cells

cells = run_cells(cells, n, m)

h = b / (n + 1)
x = np.arange(0, b , h)

if len(x) != n + 2:
 x = np.arange(0, b + h , h)

u_0 = cells[0]

u_m = cells[m]

plt.plot(x, u_0, linewidth=2, label="шаг = 0")

plt.plot(x, u_m, linewidth=2, label="шаг = "+str(m))
plt.legend()
plt.show()