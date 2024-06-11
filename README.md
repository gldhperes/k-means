# K-Means

### Link para o acesso no navegador: https://gldhperes.github.io/k-means/

É um método de quantização vetorial, originalmente de processamento de sinal, é que visa partição de N observações em K clusters em que cada observação pertence ao cluster com o mais próximo médio (centros, cluster ou centroide), servindo como um protótipo do cluster. Isso resulta no particionamento de "observações" parecidas por meio do Centroid.

Em outras palavras podemos dizer que é um sistema que categoriza objetos parecidos ou com alguma semelhança.

Esse algoritmo funciona escolhendo n-centroids, que serão os modelos a serem seguidos, em seguida:
- Passo 1: Cada objeto(observação), faz um cálculo de pitágoras (c² = a² + b²) para achar o Centoride mais perto.
- Passo 2: As novas coordenadas dos centroids serão a média da soma das coordenadas x's e y's dos objetos que contem o centroide mais próximo.
- Passo 3: Verifica se conjunto de centroid é o mesmo do anterior. SE SIM então o algoritimo acaba, SE NÃO então volta para o passo 1.

https://github.com/gldhperes/k-means/assets/111309686/e10a5925-199e-4df4-a939-111495db2fae

