import React, { useEffect, useState } from 'react';
import {
  AppBar,
  Avatar,
  Box,
  Button,
  Card,
  CardContent,
  Chip,
  Container,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Divider,
  Grid,
  IconButton,
  InputAdornment,
  Paper,
  Stack,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TablePagination,
  TableRow,
  TextField,
  Toolbar,
  Tooltip,
  Typography,
} from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import Inventory2Icon from '@mui/icons-material/Inventory2';
import SouthEastIcon from '@mui/icons-material/SouthEast';
import NorthWestIcon from '@mui/icons-material/NorthWest';
import WarehouseIcon from '@mui/icons-material/Warehouse';
import EditOutlinedIcon from '@mui/icons-material/EditOutlined';
import DeleteOutlineIcon from '@mui/icons-material/DeleteOutline';
import AddIcon from '@mui/icons-material/Add';
import SearchIcon from '@mui/icons-material/Search';
import axios from 'axios';

type Product = {
  id: number;
  companyId: string;
  productName: string;
  barcode: string;
  category: string;
};

type PagedResponse<T> = {
  data: T[];
  totalCount: number;
  page: number;
  pageSize: number;
  totalPages: number;
};

type ProductFormState = {
  id?: number;
  companyId: string;
  productName: string;
  barcode: string;
  category: string;
};

const DEFAULT_COMPANY_ID = 'COMPANY-1';

const WarehousePage: React.FC = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [page, setPage] = useState(0);
  const [pageSize, setPageSize] = useState(25);
  const [totalCount, setTotalCount] = useState(0);
  const [search, setSearch] = useState('');
  const [searchInput, setSearchInput] = useState('');
  const [debouncedSearch, setDebouncedSearch] = useState('');


  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);
  const [selectedProduct, setSelectedProduct] = useState<Product | null>(null);
  const [formState, setFormState] = useState<ProductFormState>({
    companyId: DEFAULT_COMPANY_ID,
    productName: '',
    barcode: '',
    category: '',
  });

  const [summary, setSummary] = useState({
    totalProducts: 0,
    todayIn: 0,
    todayOut: 0,
    inStock: 0,
  });

  const fetchProducts = async () => {
    const currentPage = page + 1;
    const response = await axios.get<PagedResponse<Product>>(
      `https://localhost:7121/api/product/by-company/${DEFAULT_COMPANY_ID}`,
      {
        params: {
          page: currentPage,
          pageSize,
          search,
        },
      }
    );

    setProducts(response.data.data);
    setTotalCount(response.data.totalCount);

    setSummary((prev) => ({
      ...prev,
      totalProducts: response.data.totalCount,
      inStock: response.data.totalCount,
    }));
  };

  useEffect(() => {
    fetchProducts().catch(console.error);
  }, [page, pageSize, search]);

  useEffect(() => {
    const handler = setTimeout(() => {
      setSearch(debouncedSearch);
      setPage(0);
    }, 400);

    return () => clearTimeout(handler);
  }, [debouncedSearch]);


  const handleChangePage = (_: unknown, newPage: number) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
    setPageSize(parseInt(event.target.value, 10));
    setPage(0);
  };

  const handleOpenCreate = () => {
    setSelectedProduct(null);
    setFormState({
      companyId: DEFAULT_COMPANY_ID,
      productName: '',
      barcode: '',
      category: '',
    });
    setIsEditModalOpen(true);
  };

  const handleOpenEdit = (product: Product) => {
    setSelectedProduct(product);
    setFormState({
      id: product.id,
      companyId: product.companyId,
      productName: product.productName,
      barcode: product.barcode,
      category: product.category,
    });
    setIsEditModalOpen(true);
  };

  const handleOpenDelete = (product: Product) => {
    setSelectedProduct(product);
    setIsDeleteModalOpen(true);
  };

  const handleCloseEditModal = () => {
    setIsEditModalOpen(false);
  };

  const handleCloseDeleteModal = () => {
    setIsDeleteModalOpen(false);
  };

  const handleFormChange = (field: keyof ProductFormState, value: string) => {
    setFormState((prev) => ({
      ...prev,
      [field]: value,
    }));
  };

  const handleSaveProduct = async () => {
    if (!formState.companyId) {
      return;
    }

    if (formState.id) {
      await axios.post('https://localhost:7121/api/product/update', {
        Id: formState.id,
        CompanyId: formState.companyId,
        ProductName: formState.productName,
        Barcode: formState.barcode,
        Category: formState.category,
      });
    } else {
      await axios.post('https://localhost:7121/api/product/create', {
        CompanyId: formState.companyId,
        ProductName: formState.productName,
        Barcode: formState.barcode,
        Category: formState.category,
      });
    }

    setIsEditModalOpen(false);
    await fetchProducts();
  };

  const handleConfirmDelete = async () => {
    if (!selectedProduct) {
      return;
    }

    await axios.post('https://localhost:7121/api/product/delete', {
      Id: selectedProduct.id,
      CompanyId: selectedProduct.companyId,
    });

    setIsDeleteModalOpen(false);
    await fetchProducts();
  };

  const handleSearchSubmit = (event: React.FormEvent) => {
    event.preventDefault();
    setPage(0);
    setSearch(searchInput);
  };

  return (
    <Box
      sx={{
        minHeight: '100vh',
        display: 'flex',
        bgcolor: (theme) => theme.palette.grey[100],
      }}
    >
      {/* Sol mini sidebar */}
      <Box
        sx={{
          width: 72,
          bgcolor: (theme) => theme.palette.background.paper,
          borderRight: (theme) => `1px solid ${theme.palette.divider}`,
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
          py: 2,
          gap: 2,
        }}
      >
        <Avatar sx={{ bgcolor: '#b91c1c' }}>SW</Avatar>
        <Divider flexItem />
        <IconButton sx={{ color: '#b91c1c' }}>
          <Inventory2Icon />
        </IconButton>
        <IconButton color="inherit">
          <WarehouseIcon />
        </IconButton>
      </Box>

      {/* Sağ ana içerik */}
      <Box sx={{ flex: 1, display: 'flex', flexDirection: 'column' }}>
        <AppBar position="static" elevation={0} color="transparent">
          <Toolbar sx={{ px: 3 }}>
            <IconButton
              edge="start"
              color="inherit"
              aria-label="menu"
              sx={{ mr: 2, display: { xs: 'inline-flex', md: 'none' } }}
            >
              <MenuIcon />
            </IconButton>
            <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
              Smart Warehouse
            </Typography>
            <Chip
              label={`Company: ${DEFAULT_COMPANY_ID}`}
              sx={{
                borderColor: '#b91c1c',
                color: '#b91c1c',
              }}
              variant="outlined"
              size="small"
            />
          </Toolbar>
        </AppBar>

        <Container maxWidth="lg" sx={{ py: 4 }}>
          <Grid container spacing={3} mb={1}>
            <Grid item xs={12}>
              <Stack
                direction="row"
                alignItems="center"
                justifyContent="space-between"
                spacing={2}
              >
                <Box>
                  <Typography variant="h5" gutterBottom>
                    Depo Özeti
                  </Typography>
                  <Typography variant="body2" color="text.secondary">
                    Ürünlerinizin genel görünümünü ve hareketlerini takip edin.
                  </Typography>
                </Box>
              </Stack>
            </Grid>

            <Grid item xs={12} sm={3}>
              <Card
                sx={{
                  bgcolor: '#b91c1c',
                  color: '#fee2e2',
                }}
              >
                <CardContent>
                  <Stack direction="row" justifyContent="space-between" alignItems="center">
                    <Box>
                      <Typography variant="subtitle2" sx={{ opacity: 0.8 }}>
                        Toplam Ürün
                      </Typography>
                      <Typography variant="h5" sx={{ mt: 1 }}>
                        {summary.totalProducts}
                      </Typography>
                    </Box>
                    <Inventory2Icon />
                  </Stack>
                </CardContent>
              </Card>
            </Grid>
            <Grid item xs={12} sm={3}>
              <Card>
                <CardContent>
                  <Stack direction="row" justifyContent="space-between" alignItems="center">
                    <Box>
                      <Typography variant="subtitle2" color="text.secondary">
                        Bugünkü Giriş
                      </Typography>
                      <Typography variant="h5" sx={{ mt: 1 }}>
                        {summary.todayIn}
                      </Typography>
                    </Box>
                    <NorthWestIcon color="success" />
                  </Stack>
                </CardContent>
              </Card>
            </Grid>
            <Grid item xs={12} sm={3}>
              <Card>
                <CardContent>
                  <Stack direction="row" justifyContent="space-between" alignItems="center">
                    <Box>
                      <Typography variant="subtitle2" color="text.secondary">
                        Bugünkü Çıkış
                      </Typography>
                      <Typography variant="h5" sx={{ mt: 1 }}>
                        {summary.todayOut}
                      </Typography>
                    </Box>
                    <SouthEastIcon color="error" />
                  </Stack>
                </CardContent>
              </Card>
            </Grid>
            <Grid item xs={12} sm={3}>
              <Card>
                <CardContent>
                  <Stack direction="row" justifyContent="space-between" alignItems="center">
                    <Box>
                      <Typography variant="subtitle2" color="text.secondary">
                        Depodaki Ürün
                      </Typography>
                      <Typography variant="h5" sx={{ mt: 1 }}>
                        {summary.inStock}
                      </Typography>
                    </Box>
                    <WarehouseIcon color="action" />
                  </Stack>
                </CardContent>
              </Card>
            </Grid>
          </Grid>

          <Paper
            elevation={0}
            sx={{
              mt: 2,
              borderRadius: 3,
              overflow: 'hidden',
              border: (theme) => `1px solid ${theme.palette.divider}`,
            }}
          >
            <Box
              px={3}
              py={2}
              display="flex"
              flexDirection={{ xs: 'column', sm: 'row' }}
              alignItems={{ xs: 'stretch', sm: 'center' }}
              justifyContent="space-between"
              component="form"
              onSubmit={handleSearchSubmit}
              gap={2}
            >
              <Box display="flex" alignItems="center" flex={1} gap={2}>
                <TextField
                  label="Ürün ara"
                  size="small"
                  fullWidth
                  value={debouncedSearch}
                  onChange={(e) => setDebouncedSearch(e.target.value)}
                  InputProps={{
                    startAdornment: (
                      <InputAdornment position="start">
                        <SearchIcon fontSize="small" />
                      </InputAdornment>
                    ),
                  }}
                />
              </Box>
              <Box display="flex" gap={1} justifyContent={{ xs: 'flex-end', sm: 'flex-start' }}>
                <Button
                  type="submit"
                  variant="outlined"
                  startIcon={<SearchIcon />}
                  sx={{
                    borderColor: '#b91c1c',
                    color: '#b91c1c',
                    '&:hover': {
                      borderColor: '#7f1d1d',
                      bgcolor: 'rgba(185, 28, 28, 0.04)',
                    },
                  }}
                >
                  Ara
                </Button>
                <Button
                  variant="contained"
                  startIcon={<AddIcon />}
                  onClick={handleOpenCreate}
                  sx={{
                    bgcolor: '#b91c1c',
                    '&:hover': { bgcolor: '#7f1d1d' },
                  }}
                >
                  Ürün Ekle
                </Button>
              </Box>
            </Box>

            <Divider />

            <TableContainer sx={{ maxHeight: 540 }}>
              <Table stickyHeader>
                <TableHead>
                  <TableRow>
                    <TableCell>Ürün Adı</TableCell>
                    <TableCell>Barkod</TableCell>
                    <TableCell>Kategori</TableCell>
                    <TableCell align="right">İşlemler</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {products.map((product) => (
                    <TableRow
                      key={product.id}
                      hover
                      sx={{ cursor: 'pointer' }}
                    >
                      <TableCell>{product.productName}</TableCell>
                      <TableCell>{product.barcode}</TableCell>
                      <TableCell>{product.category}</TableCell>
                      <TableCell align="right">
                        <Tooltip title="Düzenle">
                          <IconButton
                            size="small"
                            sx={{ mr: 1 }}
                            onClick={() => handleOpenEdit(product)}
                          >
                            <EditOutlinedIcon fontSize="small" />
                          </IconButton>
                        </Tooltip>
                        <Tooltip title="Sil">
                          <IconButton
                            size="small"
                            color="error"
                            onClick={() => handleOpenDelete(product)}
                          >
                            <DeleteOutlineIcon fontSize="small" />
                          </IconButton>
                        </Tooltip>
                      </TableCell>
                    </TableRow>
                  ))}
                  {products.length === 0 && (
                    <TableRow>
                      <TableCell colSpan={4} align="center">
                        Kayıt bulunamadı.
                      </TableCell>
                    </TableRow>
                  )}
                </TableBody>
              </Table>
            </TableContainer>

            <TablePagination
              component="div"
              count={totalCount}
              page={page}
              onPageChange={handleChangePage}
              rowsPerPage={pageSize}
              onRowsPerPageChange={handleChangeRowsPerPage}
              rowsPerPageOptions={[10, 25, 50]}
            />
          </Paper>

          <Dialog open={isEditModalOpen} onClose={handleCloseEditModal} fullWidth maxWidth="sm">
            <DialogTitle>
              {formState.id ? 'Ürün Düzenle' : 'Ürün Ekle'}
            </DialogTitle>
            <DialogContent>
              <Box mt={1} display="flex" flexDirection="column" gap={2}>
                <TextField
                  label="Ürün Adı"
                  value={formState.productName}
                  onChange={(e) => handleFormChange('productName', e.target.value)}
                  fullWidth
                />
                <TextField
                  label="Barkod"
                  value={formState.barcode}
                  onChange={(e) => handleFormChange('barcode', e.target.value)}
                  fullWidth
                />
                <TextField
                  label="Kategori"
                  value={formState.category}
                  onChange={(e) => handleFormChange('category', e.target.value)}
                  fullWidth
                />
              </Box>
            </DialogContent>
            <DialogActions>
              <Button onClick={handleCloseEditModal}>Vazgeç</Button>
              <Button
                variant="contained"
                onClick={handleSaveProduct}
                sx={{
                  bgcolor: '#b91c1c',
                  '&:hover': { bgcolor: '#7f1d1d' },
                }}
              >
                Kaydet
              </Button>
            </DialogActions>
          </Dialog>

          <Dialog open={isDeleteModalOpen} onClose={handleCloseDeleteModal}>
            <DialogTitle>Ürünü sil</DialogTitle>
            <DialogContent>
              <Typography>
                {selectedProduct
                  ? `"${selectedProduct.productName}" adlı ürünü silmek istediğinize emin misiniz?`
                  : 'Bu ürünü silmek istediğinize emin misiniz?'}
              </Typography>
            </DialogContent>
            <DialogActions>
              <Button onClick={handleCloseDeleteModal}>Vazgeç</Button>
              <Button color="error" variant="contained" onClick={handleConfirmDelete}>
                Sil
              </Button>
            </DialogActions>
          </Dialog>
        </Container>
      </Box>
    </Box>
  );
};

export default WarehousePage;

