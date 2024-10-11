export class ShopParams {
  brands: string[] = [];
  types: string[] = [];
  sort?: string;
  pageNumber = 1;
  pageSize = 10;
  searchTerm?: string;
}
